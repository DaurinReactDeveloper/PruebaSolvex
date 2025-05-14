<img src='https://i.imgur.com/oV7AZCy.png' alt="Backend" style="width:100%; border-radius: 10px;"/>

<h1 align="center">🛠️ Backend del Sistema de Gestión de Productos</h1>

<h2>📌 Descripción General</h2>

<p>
Este proyecto fue desarrollado con <strong>ASP.NET Core (.NET 8)</strong> siguiendo una arquitectura limpia. Se conecta a una base de datos <strong>MySQL</strong> y permite la gestión completa de productos, autenticación de usuarios con <strong>JWT</strong>, encriptación de contraseñas y carga de imágenes en la nube con <strong>Cloudinary</strong>.
</p>

---

<h2>🧠 Lógica de Productos con Colores y Precios Diferentes</h2>

<p>
🔄 Para permitir productos con múltiples precios según el color, se implementó una <strong>restricción única</strong> en la base de datos que impide que se repita la combinación <code>nombre + color</code>.
</p>

<p>✅ Ejemplo de registros válidos:</p>
<ul>
  <li>📱 IPhone - Negro - $1200</li>
  <li>📱 IPhone - Azul - $1200</li>
  <li>📱 IPhone - Verde - $1300</li>
</ul>

<p>🛡️ Esto garantiza integridad y variedad sin duplicar productos incorrectamente.</p>

---

<h2>🖼️ Gestión de Imágenes con Cloudinary</h2>

<p>
🧩 Al agregar o editar un producto, el usuario puede subir una imagen. Esta imagen es almacenada en la nube usando <strong>CloudinaryDotNet</strong> y se guarda la URL correspondiente en la base de datos.
</p>

<h3>🚀 Flujo:</h3>
<ol>
  <li>🧑‍💻 El usuario selecciona una imagen en el frontend.</li>
  <li>📤 El backend la recibe como <code>IFormFile</code> y la sube a Cloudinary.</li>
  <li>🔗 Se obtiene la URL de la imagen y se guarda en el registro del producto.</li>
</ol>

---

<h2>🗂️ Esquema de Base de Datos</h2>

<p>A continuación se muestra el diseño de la base de datos utilizado para garantizar integridad y eficiencia:</p>

<p align="center">
  <!-- Reemplaza este link con tu imagen real del diagrama de BD -->
  <img src="https://i.imgur.com/tuImagenEjemplo.png" alt="Esquema BD" style="max-width: 100%; border: 1px solid #ccc; border-radius: 8px;" />
</p>

---

<h2>⚙️ Tecnologías Usadas</h2>

<ul>
  <li>🌐 <strong>ASP.NET Core (.NET 6)</strong></li>
  <li>🛢️ <strong>MySQL</strong> con Entity Framework Core</li>
  <li>🔐 <strong>JWT</strong> (Microsoft.AspNetCore.Authentication.JwtBearer)</li>
  <li>🔒 <strong>BCrypt.Net-Next</strong> para encriptar contraseñas</li>
  <li>☁️ <strong>CloudinaryDotNet</strong> para imágenes</li>
  <li>🏗️ Arquitectura en capas: Controllers, Services, Repositories</li>
</ul>

---

<h2>🧰 Servicios Implementados</h2>

<ul>
  <li><strong>🧾 AuthService</strong>: Registro, login, autenticación y generación de tokens JWT.</li>
  <li><strong>📦 ProductService</strong>: CRUD completo de productos con lógica para colores y precios únicos.</li>
  <li><strong>👥 UserService</strong>: Gestión de usuarios, roles y validaciones.</li>
  <li><strong>🖼️ ImageService</strong>: Subida de imágenes a Cloudinary y gestión de rutas.</li>
</ul>

---

<h2>🔐 Roles y Acceso</h2>

<p>El sistema cuenta con un sistema robusto de autorización por roles:</p>

<table>
  <thead>
    <tr>
      <th>👤 Rol</th>
      <th>📝 Permisos</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td><strong>Administrador</strong></td>
      <td>Ver, Crear, Editar, Eliminar</td>
    </tr>
    <tr>
      <td><strong>Vendedor</strong></td>
      <td>Ver, Crear, Editar</td>
    </tr>
    <tr>
      <td><strong>Usuario</strong></td>
      <td>Ver</td>
    </tr>
  </tbody>
</table>

---

<h2>📫 Contacto</h2>

<p>👨‍💻 Desarrollado por: <strong>[Tu nombre o equipo]</strong></p>
<p>📧 Email: <a href="mailto:tuemail@dominio.com">tuemail@dominio.com</a></p>
