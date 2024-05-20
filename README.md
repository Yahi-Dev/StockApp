OJO: Al momento de alguien bajar los cambios debe de cambiar solamente del appsettings.json (en el proyecto de WebApi y del WebApp) el IdentityConnection y el DefaultConnetion con la ruta de su base de datos,
luego vas a Package Manager Console y en la opción que dice Default Projects  lo vas a poner primero en la capa de identity y vas a escribir: Update-Database -Context IdentityContext,
Lo mismo vas hacer con la capa de persistence, vas a cambiar de la capa de identity a persistencia y en el panes de escritura vas a escribir: Update-Database -Context ApplicationContext.
Sino haces estos cambios no te va a funcionar la app y te va a dar un error de mapeo con la base de datos.
