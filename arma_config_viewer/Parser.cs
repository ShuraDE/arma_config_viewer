using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arma_config_viewer
{


    class ConfigEntry
    {
        //parser props
        string activeProp = "";
        bool isInParseMode = true;
        bool commentActive = false;
        int bracketCounter = 0;
        bool lineActive = false;
        //data props
        ConfigEntry parent;
        ConfigEntry inheritClass;
        List<ConfigEntry> childs = new List<ConfigEntry>();
        string className = "";
        string classInherit = "";
        Dictionary<string, string> data_prop = new Dictionary<string, string>();
        Dictionary<string, List<string>> data_props = new Dictionary<string, List<string>>();
        List<string> unknownEntries = new List<string>();


        ConfigEntry activeObj;

        public List<ConfigEntry> Inherits()
        {
            List<ConfigEntry> inherits = new List<ConfigEntry>();
            inherits.Add(this);
            if (inheritClass != null)
            {
                inherits.AddRange(inheritClass.Inherits());
            }

            return inherits;
        }

        public List<ConfigEntry> Parents ()
        {
            List<ConfigEntry> parents = new List<ConfigEntry>();
            parents.Add(this);
            if (parent != null)
            {
                parents.AddRange(parent.Parents());
            }

            return parents;
        }

        public bool IsInParseMode
        {
            get
            {
                return isInParseMode;
            }

            set
            {
                isInParseMode = value;
            }
        }

        public string ClassWithInheritName
        {
            get {
                return this.className + " : " + this.classInherit;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }

            set
            {
                className = value;
            }
        }

        public string ClassInherit
        {
            get
            {
                return classInherit;
            }

            set
            {
                classInherit = value;
            }
        }

        internal List<ConfigEntry> Childs
        {
            get
            {
                return childs;
            }

            set
            {
                childs = value;
            }
        }

        public Dictionary<string, string> Data_prop
        {
            get
            {
                return data_prop;
            }

            set
            {
                data_prop = value;
            }
        }

        public Dictionary<string, List<string>> Data_props
        {
            get
            {
                return data_props;
            }

            set
            {
                data_props = value;
            }
        }

        internal ConfigEntry Parent
        {
            get
            {
                return parent;
            }

            set
            {
                parent = value;
            }
        }

        internal ConfigEntry InheritFromClass
        {
            get
            {
                return inheritClass;
            }

            set
            {
                inheritClass = value;
            }
        }

        internal ConfigEntry InheritClass
        {
            get
            {
                return inheritClass;
            }

            set
            {
                inheritClass = value;
            }
        }

        private void initLine(string line)
        {
            //z.B. 	class UnitMilitiaSport_A: UnitCivSport_A
            line = line.Replace('\t',' ').Trim(); //remove spaces and tabs
            string[] words = line.Split(' ');

            if (words.Length > 2)
            {
                if (words[1].IndexOf(':') > 0)
                {
                    className = words[1].Substring(0, words[1].IndexOf(':'));
                } else
                {
                    className = words[1];
                }

                if (!words[2].Equals(':'))
                {
                    classInherit = words[2];
                } else if (words.Length >3)
                {
                    classInherit = words[3];
                }

            } else //simple class
            {
                if (words[1].IndexOf(';') > 0)
                {
                    className = words[1].Substring(0, words[1].IndexOf(';'));
                }
                else {
                    className = words[1];
                }
            }

            if (line.EndsWith(";"))
            {
                isInParseMode = false;
            }
            else {

                bracketCounter = getOpenBracketCount(line);
            }


        }

        internal int getOpenBracketCount(string line)
        {
            int n = 0;
            bool strMarker = false;
            foreach (char c in line)
            {
                if (c.Equals('"')) strMarker = !strMarker;
                if (c.Equals('{') && !strMarker) n++;
                if (c.Equals('}') && !strMarker) n--;
            }

            return n;
        }

        internal ConfigEntry(string line)
        {
            initLine(line);
        }

        internal ConfigEntry(string line, ConfigEntry parent)
        {
            initLine(line);
            this.parent = parent;
        }

        internal void NewLine(string line) //return true if class is closed
        {
            //move to child if active
            if (activeObj != null && activeObj.isInParseMode)
            {
                activeObj.NewLine(line);
            }
            //check new class starts here
            else if (line.Contains("class ") && bracketCounter > 0)
            {
                if (activeObj == null)
                {
                    activeObj = new ConfigEntry(line, this);
                    childs.Add(activeObj);
                }
                else
                {
                    if (activeObj.isInParseMode)
                    {
                        activeObj.NewLine(line);
                    }
                    else
                    {
                        activeObj = new ConfigEntry(line, this);
                        childs.Add(activeObj);
                    }
                }
            }
            //array prop is active
            else if (!String.IsNullOrEmpty(activeProp))
            {
                if (line.Equals("};"))
                {
                    activeProp = "";
                }
                else {
                    data_props[activeProp].Add(line);
                    if (line.EndsWith("};")) activeProp = "";
                } 
            } else
            {
                if ((line.IndexOf("[]=") < line.IndexOf('=') && line.IndexOf("[]=") > 0) || (line.IndexOf("[]+=") < line.IndexOf('=') && line.IndexOf("[]+=") > 0)) //array startet
                {
                    if (line.IndexOf("[]=") > 0) activeProp = line.Substring(0, line.IndexOf("[]="));
                    if (line.IndexOf("[]+=") > 0) activeProp = line.Substring(0, line.IndexOf("[]+="));
                    data_props.Add(activeProp, new List<string>());
                }
                else if (line.IndexOf("=") > 0)
                {
                    string[] val = line.Split('=');
                    if (val[1].IndexOf(";")>0)
                    {
                        data_prop.Add(val[0], val[1].Substring(0, val[1].IndexOf(";")));
                    } else if (line.LastIndexOf(';') == line.Length)
                    {
                        data_prop.Add(val[0], line.Substring(val[0].Length - 1, line.Length - val[0].Length));
                    } else
                    {
                        data_prop.Add(val[0], val[1]);
                    }
                    
                }
                else
                {
                    unknownEntries.Add(line);
                }
            }


            bracketCounter += getOpenBracketCount(line);
            isInParseMode = bracketCounter > 0;
        }

        internal void AddComment(string line)
        {

        }
    }

    class ConfigFile
    {
        List<ConfigEntry> entries = new List<ConfigEntry>();
        List<string> unknownEntries = new List<string>();

        internal List<ConfigEntry> Entries
        {
            get
            {
                return entries;
            }

            set
            {
                entries = value;
            }
        }

        internal void NewLine(string line)
        {

            line = line.Replace('\t', ' ').Trim();

            if (line.Contains("class ") && ((entries.Count > 0 && !entries[entries.Count-1].IsInParseMode) || entries.Count==0 ))
            {
                entries.Add(new ConfigEntry(line));

            } else if (entries.Count > 0) 
            {
                entries[entries.Count - 1].NewLine(line);
            } else
            {
                unknownEntries.Add(line);
            }
        }
    }



    class Parser
    {

        ConfigFile configFile = new ConfigFile();

        internal ConfigFile ConfigFile
        {
            get
            {
                return configFile;
            }

            set
            {
                configFile = value;
            }
        }

        private Parser() { }
        public Parser(string file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                configFile.NewLine(line);
            }


            //post parsing operations

            //reference inheritance classes
            foreach (ConfigEntry entry in configFile.Entries)
            {
                if (entry.Childs.Count > 0) CheckEachObject(entry.Childs, entry);
            }

        }

        private void CheckEachObject(List<ConfigEntry> childs, ConfigEntry root)
        {
            foreach (ConfigEntry child in childs)
            {
                if (!String.IsNullOrEmpty(child.ClassInherit))
                {
                    child.InheritFromClass = GetInheritanceClassObject(root, child.ClassInherit);
                }
                if (child.Childs.Count > 0) CheckEachObject(child.Childs, root);
            }
        }

        private ConfigEntry GetInheritanceClassObject(ConfigEntry root, string className)
        {
            ConfigEntry retVal;

            foreach (ConfigEntry entry in root.Childs)
            {
                if (className.Equals(entry.ClassName)) { return entry; }
            }

            foreach (ConfigEntry entry in root.Childs)
            {
                retVal = GetInheritanceClassObject(entry, className);
                if (retVal != null) return retVal;
            }

            return null;

        }
    }
}
