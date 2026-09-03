# Overview

## Definitions
1. **"Connected Application"**
	a. TextResource context: connected through table `ClientApplicationTextResource`
	b. Translation context: connected through tables `ClientApplicationTextResource` and `ClientApplicationLocale`
2. **Active Application**: A ClientApplication with `IsActive = true`
3. **Restricted Principal**: A user that is connected to either certain Applications, and/or certain Locales
4. **Unrestricted Principal**: A user that is not connected to any Applications or Locales
5. **"Missing Translation"**: A Translation that is connected to at least one active Application, but falls back to the "undefined" Locale
6. **Terminology**
	a. **Application**: a `ClientApplication` entity
	b. **Locale**: a `Locale` entity
	c. **Text**: a `TextResource` entity
	d. **Translation**: a `Translation` entity
	e. **Flat Translation**: a `flat.Translation` entity

## Functionality
1. Require authentication for the API
	a. Users can have one or more of the following roles:
		i. **SysAdmin**
			- Full administrative access, but still subject to data-integrity rules below
			- Can CRUD Applications, Texts, Formalities, Presentations, Translations
			- Can assign roles and Application/Locale restrictions to users
			- Is always unrestricted
		ii. **AppAdmin**
			- Can manage one or more assigned Applications
			- Can CRUD `ClientApplicationLocale` and `ClientApplicationTextResource` entries for assigned Applications
			- Ignores Locale restrictions for Application administration purposes
		iii. **Translator**
			- Can CRUD Texts and Translations within the permitted Application/Locale scope
			- Can be restricted to certain Applications and/or Locales
			- If restricted only by Application, can access all Texts connected to the permitted Application(s)
			- If restricted only by Locale, can access all Texts connected to Applications using the permitted Locale(s)
			- If restricted by both, can access only Texts connected to a permitted Application that also uses a permitted Locale
		iv. **Consumer**
			- Can only read Flat Translations within the permitted Application/Locale scope
			- Can be restricted to certain Applications and/or Locales
	b. Roles can be combined; in particular, a user can be both an Appadmin, and a Translator
	c. A Restricted Principal can only access data for their connected Applications and/or Locales; if restricted by both Applications and Locales, access is limited to the intersection of both restrictions
	d. An Unrestricted Principal has access to all Applications and Locales permitted by their assigned role(s)
	e. A Text or Translation can only be deleted if it is not connected to any Active Application
	f. A Translation can only be modified if it is connected to no more than one Active Application
	g. Locales can never be created, modified or deleted by any user; they represent a fixed set
	h. An Application can only be deleted if there are no connected Translations
		- According to the Connected Application definition
		- Connected entries are deleted together with the Application
	i. Locales, Formalities, and Presentations are readable by every user
	j. Authentication source: Existing Azure AD B2C
	k. When a new user authenticates who is not yet in the `Principal` table, add them there with Role=0
2. The same authentication/authorization rules as defined in 1. must be applied to the Web GUI ("Textbase.Host"), once it is being implemented.
3. Implement Web GUI ("Textbase.Host")
	a. View/Create/Edit Applications, Application Locales, Application TextResources
	b. View/Create/Edit Formalities
	c. View Locales
	d. View/Create/Edit Presentations
	e. View/Create/Edit TextResources
	f. View/Create/Edit Translations
	g. User administration / role assignments (only for Sysadmin users)
		- Add a badge containing the number of active users with Role=0 to the link
	h. Get an overview of missing translations, filtered by Application and/or Locale
	i. Import/Export functionality (*to be specified*)
	j. Use Radzen components, and the Uwn.Blazor package

## Known Issues
1. `flat.Translation` is treated like a normal table, while it should be read-only
