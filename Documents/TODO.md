1. Require authentication for the API
	a. User privileges/roles must allow to distinct between users who can read from flat translations only, users who can modify existing translations, and users who can add new TextResources and translations.
	b. It must be possible to attach Locales and Applications to users; such "restricted" users can only access data connected to the attached Locales/Applications.
	c. Maximum possible: Read/Modify/Create/Delete for all Locales and Applications
	d. Minimum possble: Read a single Locale and Application
	e. A TextResource/Translation can only be deleted if it is not connected to any active Application
	f. A Translation can only be modified if it is connected to no more than one active Application
2. Them same authentication/authorization rules as defined in 1. must be applied to the Web GUI ("Textbase.Host"), once it is being implemented
3. Implement Web GUI ("Textbase.Host")
	a. View/Create/Edit Applications, Application Locales, Application TextResources
	b. View/Create/Edit Formalities
	c. View/Create/Edit Locales
	d. View/Create/Edit Presentations
	e. View/Create/Edit TextResources
	f. View/Create/Edit Translations
	g. Get an overview of missing translations, filtered by Application and/or Locale
	h. Import/Export functionality (*to be specified*)
