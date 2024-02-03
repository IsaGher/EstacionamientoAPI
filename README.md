# Consideraciones que se tuvieron:

## Tabla PARKING_RECORD
Aquí se guardan los registros de los vehículos que entran al estacionamiento, para el caso de uso cierre de mes que borraba las estancias de los vehículos oficiales se pensaba manejar la columna ``isActive`` como un indicador si estaba activo el registro o ya no (es de mes pasado) para no borrar los registros de la base que pueden ser de ayuda más adelante.