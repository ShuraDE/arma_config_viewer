# arma_config_viewer

Small tool to view classes from a config.cpp for arma (game or mods).<br/>
Treeview on left side is hierarchical from config<br/>
Listview in right side, contains all listed properties in config incl. all known inherit classes.<br/>

Treeview roots are config classes like "cfgVehicle", "cfgWeapons" ....

red marked lines: active object value changed from inherit classes


//not done now:
* Listview refresh only on mouse click
* array values are not in listview (but in code)
* diff between 2 config files
* showing (marked) inherited properties directly on selected class
* column size reset after new object selection
* showing active config source
* model preview (maybe never)
* paa preview (maybe never)
* editor functionality (maybe never)


<img src="bsp.PNG">
