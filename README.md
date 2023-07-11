# SistemaAsociados
Pequeño demo de un sistema que implica Asociados-Departamento, en este sistema se pueden crear nuevos departamentos y aplicar aumentos, el sistema en automático toma ese aumento y lo aplica a todos los asociados que pertenecen a dicho departamento. La base de datos consta de 4 tablas: Menu, Departamento, Asociado, Usuario.

# Iniciar Sesion
Para iniciar sesión los datos de prueba son los siguientes: <br/>
`email: admin@mail.com` <br/>
`clave: admin`

# Versiones
Las versiones para crear este demo son las siguientes: <br/>
`Angular CLI: 16.0.3` <br/>
`Node: 18.16.0` <br/>
`Package Manager: npm 9.6.7` <br/>
`.NET 7.0 ` <br/>
`Visual Studio 2022 (17.6.2) ` <br/>
`Microsoft SQL Server 2022 (RTM-GDR) (KB5021522) - 16.0.1050.5 (X64)` <br/>

** Para cada versión de los paquetes de la aplicación angular, revisar el archivo `package.json` dentro de AppSistemaAsociados.

# Cómo Instalar/Restuarar
<ol>
  <li>Clonar o descargar todo el repositorio</li>
  <li>Restaurar el archivo de la base de datos (Asociado_Salario.bak) o ejecutar el script (Script for DataBase)</li>
  <li>Abrir la solución dentro Visual Studio 2022 y restaurar los paquetes Nuget</li>
  <li>Ejectuar la solución para verificar que todas las Api están funcionales</li>
  <li>Abrir la carpeta AppSistemaAsociados con VSCode o VSCodium, restaurar los paquetes node (npm install) </li>
  <li>Ejecutar la aplicación angular (ng serve -o) y hacer login para verificar el funcionamiento del sistema</li>
</ol>
