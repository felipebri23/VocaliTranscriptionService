> **Problema**\
> El servicio de reconocimiento de voz de INVOX Medical permite la
> transcripción de ficheros en formato MP3 que hayan sido grabados por
> usuarios registrados en el
> sistema.![](vertopal_3bbb1ed9961e40478a4fecaf1a223485/media/image1.png){width="4.493055555555555in"
> height="3.1775885826771653in"}
>
> En un hospital, ya se graban todos los informes dictados por los
> médicos en ficheros de audio en formato MP3. Quiere utilizarse INVOX
> Medical para la transcripción de estos ficheros y su posterior
> supervisión.
>
> Para ello, se construirá un servicio o aplicación de Windows que se
> encargará de enviar los ficheros de audio grabados al servicio de
> reconocimiento de voz de INVOX Medical.
>
> La aplicación se implementará en C# y deberá cumplir con los
> siguientes casos de uso.
>
> **Casos de uso**\
> Envío de ficheros para transcribir
>
> **Descripción**: A las 00:00 de cada noche el servicio enviará, en
> bloques de 3, todos los ficheros disponibles para transcripción al
> servicio de transcripción de ficheros de INVOX Medical.
>
> Las peticiones al **servicio de transcripción de ficheros de INVOX
> Medical** se enviarán de 3 en 3. Serán tres peticiones distintas
> simultáneas. Cuando una termine empezará otra pero nunca podrá haber
> más de 3 peticiones ejecutándose de manera simultánea.
>
> Estos ficheros inicialmente se encontrarán en una ruta previamente
> inyectada al servicio, junto con una lista de políticas de validación
> de los ficheros.
>
> El servicio verificará antes de enviar que los audios cumplen las
> siguientes políticas de validación de ficheros:\
> - El fichero debe tener un tamaño mínimo de 50KB y un tamaño máximo de
> 3MB.
>
> \- El fichero debe tener formato mp3 válido.
>
> El servicio recibe el resultado del **servicio de transcripción de
> ficheros de INVOX Medical** y lo guarda en la misma ruta donde se
> encontraba el fichero de audio y con igual nombre pero extensión .txt.
> Si una petición devuelve un error, la repite hasta un máximo de 3
> veces.
>
> **Precondiciones**:\
> 1. El servicio de transcripción de ficheros de INVOX Medical está
> disponible.
>
> **Actor**: servicio\
> **Flujo principal**:\
> 1. A las 00:00 el servicio selecciona el primer fichero disponible y
> lo envía al servicio de transcripción de ficheros de INVOX Medical,
> indicando el login del usuario para que INVOX Medical sepa qué perfil
> de voz ha de usar para la transcripción.
>
> 2. El servicio recibe el resultado y lo guarda en la misma ruta donde 
> se encontraba el fichero de audio y con igual nombre pero extensión .txt. 
> Si una petición devuelve un error, la repite hasta un máximo de 3 veces.
>
> 3. Repite las acciones anteriores hasta que no queda ningún fichero
> por enviar.
>
> **Poscondiciones**:
>
> 1. Se ha almacenado el resultado de la transcripción de todos los
> ficheros recibidos en el día anterior.
>
> **Servicio de transcripción de ficheros de INVOX Medical** Se simulará
> la existencia de este servicio mediante un mock up con las siguientes
> restricciones:
>
> 1. Existirán 4 textos predefinidos (cualesquiera) posibles que
> siempre se devolverán para una transcripción exitosa sea cual sea el
> contenido del fichero MP3. Se creará una función que seleccionará el
> texto a elegir en función del contenido del fichero enviado de manera
> que cualquiera de los 4 textos tenga la misma probabilidad de ser
> elegido.
>
> 2. El mock up devolverá un error genérico el 5% de las veces que sea invocado.
> **Aspectos de diseño y construcción**
>
1. El sistema se construirá con tecnologías .NET, en concreto C#.
2. Se utilizará el IDE Visual Studio 2019 Community.
3. Se puede usar cualquier tecnología dentro de las disponibles en .NET.
4. Se puede usar cualquier componente o librería open source con el uso de NuGet.
5. La solución dispondrá de test unitarios.          
6. El servicio ha de generar log para facilitar el diagnóstico.        
7. El código se comentará siguiendo las convenciones de C#.                 
8. Se utilizará el framework .NET Core 3.0.                |
9. Se valorará la limpieza de código. 
10. Opcionalmente, se puede utilizar Git para llevar un control de cambios
