<img src='https://i.imgur.com/oV7AZCy.png' alt="Backend"/>

<h1>🔧 Backend del Sistema de Gestión de Productos</h1> 

<h2>🧠 Descripción del Proyecto (Backend)</h2> 

<p>Desarrollado con <strong>ASP.NET Core</strong> y estructurado bajo principios de arquitectura limpia, este backend gestiona productos, usuarios y roles utilizando autenticación segura con <strong>JWT</strong>. Se conecta a una base de datos <strong>MySQL</strong> y permite la carga de imágenes con <strong>Cloudinary</strong>.</p>

<p>El sistema permite múltiples variaciones de un producto según su <strong>color</strong>, con precios diferentes por cada uno. Los usuarios pueden autenticarse y ver funcionalidades según su <strong>rol</strong>.</p>

<h2>🧠 Lógica de Solución para Productos con Diferentes Colores y Precios</h2> 

<p>Para permitir que un mismo producto tenga múltiples precios por color, se implementó una restricción en la base de datos que garantiza que la <strong>combinación de nombre y color sea única</strong>.</p>

<p>De esta manera, podemos tener registros como:</p>

<ul>
<li>IPhone - Negro - $1200</li>
<li>IPhone - Azul - $1200</li>
<li>IPhone - Verde - $1300</li>
</ul>

<p>Esto se logra mediante una restricción <strong>UNIQUE(nombre, color)</strong> en la tabla de productos o en una tabla de variaciones si se usa una estructura más normalizada.</p>

<h2>🖼️ Subida y Gestión de Imágenes</h2>

<p>Para manejar imágenes de productos, se utilizó <strong>CloudinaryDotNet</strong>. Al registrar o editar un producto, se puede subir una imagen, la cual es enviada a Cloudinary y se guarda la URL en la base de datos.</p>

<h3>🧩 Proceso de subida de imagen:</h3>
<ol>
<li>El usuario selecciona una imagen desde el frontend.</li>
<li>La imagen se envía como <code>IFormFile</code> al backend.</li>
<li>Se usa el SDK de Cloudinary para subirla con una configuración segura.</li>
<li>Se obtiene la URL y se guarda en la base de datos como parte del producto.</li>
</ol>

<p>De esta manera, las imágenes no se almacenan directamente en el servidor ni en la base de datos, solo su URL.</p>

<h2>🛠 Tecnologías Utilizadas</h2> 
<ul> 
<li>⚙️ ASP.NET Core (.NET 6+)</li>
<li>🧱 MySQL con Entity Framework Core</li>
<li>🔐 JWT (Microsoft.AspNetCore.Authentication.JwtBearer)</li>
<li>🔒 BCrypt.Net-Next para encriptación de contraseñas</li>
<li>☁️ CloudinaryDotNet para gestión de imágenes</li>
<li>📁 Arquitectura en capas</li>
</ul>

<h2>🧰 Servicios Backend</h2> 
<ul>
<li><strong>AuthService</strong>: Registro, login, autenticación JWT y asignación de roles.</li>
<li><strong>ProductService</strong>: Mantenimiento completo de productos, incluyendo variaciones por color y precio únicos.</li>
<li><strong>UserService</strong>: CRUD de usuarios, visualización de perfiles y asignación de roles.</li>
<li><strong>ImageService</strong>: Carga y gestión de imágenes con Cloudinary.</li>
</ul>

<h2>🛡 Seguridad Basada en Roles</h2>
<ul>
<li><strong>Admin</strong>: Crear, Modificar, Eliminar, Ver</li>
<li><strong>Seller</strong>: Crear, Modificar, Ver</li>
<li><strong>User</strong>: Ver</li>
</ul>

