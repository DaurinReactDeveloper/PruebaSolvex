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

<h2>🗂️ Esquema de Base de Datos</h2>

<p>A continuación se muestra el diseño de la base de datos utilizado para garantizar integridad y eficiencia:</p>

<p align="center">
  <img src="https://res.cloudinary.com/dret6llu8/image/upload/v1747261594/l1ckwdh7iskdl0s4pltg.png" alt="Esquema BD" style="max-width: 100%; border: 1px solid #ccc; border-radius: 8px;" />
</p>

---

<h2>⚙️ Tecnologías Usadas</h2>

<ul>
  <li>🌐 <strong>ASP.NET Core (.NET 8)</strong></li>
  <li>🛢️ <strong>MySQL</strong> con Entity Framework Core</li>
  <li>🔐 <strong>JWT</strong> (Microsoft.AspNetCore.Authentication.JwtBearer)</li>
  <li>🔒 <strong>BCrypt.Net-Next</strong> para encriptar contraseñas</li>
  <li>☁️ <strong>CloudinaryDotNet</strong> para imágenes</li>
  <li>🏗️ Arquitectura Limpia</li>
</ul>

---

<h2>🧰 Servicios Implementados</h2>

<ul>
  <li><strong>🧾 CloudinaryService</strong>: Subida y gestión de imágenes en Cloudinary, incluyendo carga y eliminación de imágenes.</li>
  <li><strong>🔐 JwtService</strong>: Generación y validación de tokens JWT para autenticación de usuarios.</li>
  <li><strong>🔒 PasswordHelperService</strong>: Encriptación y verificación de contraseñas usando BCrypt.</li>
  <li><strong>📦 ProductService</strong>: CRUD completo de productos con lógica para colores y precios únicos.</li>
  <li><strong>👥 UserService</strong>: Gestión de usuarios, roles, validación de credenciales y asignación de roles.</li>
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

<p>👨‍💻 Desarrollado por: <strong>Daurin Gonzalez</strong></p>
<p>📧 Email: dauringonzales7@gmail.com</p>
