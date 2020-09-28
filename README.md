# LABORATORIO #2

### **Objetivos:**

- Aplicar los conceptos de Árboles B
- Almacenar archivos en disco
- Verbo DELETE

***Kevin Romero 1047519***

***José De León   1072619***

---

### Contenido

- Implementación de un árbol b genérico de grado variable en disco
- Implementación del un árbol b de enteros en consola con almacenamiento en disco
- Implementación del un árbol b de películas en una API con almacenamiento en disco

---

### Uso del Proyecto de Consola

Al compilar la clase ***program*** observará los resultados de los recorridos *inorden*, *preorden* y *postorden* de una secuencia de enteros previamente agregada al árbol b de grado tres. Los resultados de la inserción y eliminación los podrá observar en el archivo de texto data.txt dentro de la solución del proyecto en consola.

---

### Uso de la API

1. Haga una petición **POST** en ***api/movies*** agregando en el apartado raw del body lo siguiente

    ```json
    {
    	"order": 5
    }
    ```

    Con esto se incializará un árbol del grado específicado, no menor a grado dos.

2. Haga una petición **POST** en ***api/movies/populate*** agregando en el apartado form-data el archivo Json de prueba, la llave con la que se ingresa el archivo deberá llamarse **file**
3. Haga una petición **GET** en ***api/movies/{traversal}*** intercambiando el parámetro de la petición por el recorrido desado (ej: ***api/movies/inorden*** , ***api/movies/preorden*** , ***api/movies/postorden***)

Para volver a iniciar el árbol con distinto grado, repita la secuencia anterior variando el grado ingresado en el inciso uno.

4. Haga una petición **DELETE** en ***api/movies/populate/{id}*** intercambiando el parámetro de la petición por el id del valor a eliminar, el id de un valor se compone por la unión del titulo con el año de estreno (titulo-año) (ej: ***api/movies/populate/Following-1999***)

5. Haga una petición **DELETE** en ***api/movies*** para eliminar por completo el contenido de la estructura, al realizar esta petición tambien se eliminarán de forma física los registros previamente ingresados y el archivo volvera a su estado original. 
 
Para volver a iniciar el árbol con distinto grado, repita la secuencia anterior variando el grado ingresado en el inciso uno.

Para verificar el estado del árbol b en cualquier instante puede abrir el archivo **data.txt** que se encuentra dentro de la solución de API.
