14/06:

Animación de salto no funciona, cambié el paramétro de Vertical a Jump y en vez de hacerlo en float lo hice en un Bool (más simple supongo) pero aún así no funcionaba, terminé descubriendo que es porque el piso no está con isTrigger (tuve que usar debug para descubrirlo) pero al activar el isTrigger la colisión no funciona supongo y el Player se cae del piso.
Creé variables en el script que está viendo todo lo del salto y su animación (PlayerController) para que detectara que el piso tuviera tanto el Tag como el Layer "Ground", no sé si me traiga más errores pero ahí está.
Logré conectar la animación de Run y que funcionara correctamente todo lo relacionado con correr (multiplicador de velocidad/activación cuando presionas dirección).