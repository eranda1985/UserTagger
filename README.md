# UserTagger
Urban Airship User Tagger

## Add new tags.
Please follow through these steps for each new tag. 
1. Add a record in Tags table under UniSAStudentApp Database with *ToIstall* as **true** and *IsNew* as **true**.
2. Create a handler under Handlers folder.
3. Then create necessary Repository classes to generate the relevant uid list for that tag.
4. Then create a Handler plugin in Plugins folder. 
5. Register the plugin in App.config file. 
