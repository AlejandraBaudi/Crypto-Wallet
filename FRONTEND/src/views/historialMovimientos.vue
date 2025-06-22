<template>
  <div class="fondo">
    <div class="historial">
      <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
      <h2>Historial de Movimientos</h2>
      <table>
        <thead>
          <tr>
            <th>Fecha</th>
            <th>Accion</th>
            <th>Criptomoneda</th>
            <th>Cantidad</th>
            <th>Valor</th>
            <th>Acciones</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="mov in movimientos" :key="mov.id">
            <td>{{ new Date(mov.dateTime).toLocaleString() }}</td>
            <td>{{ mov.action }}</td>
            <td>{{ mov.cryptoCode }}</td>
            <td>{{ mov.cryptoAmount }}</td>
            <td>${{ mov.money }}</td>
            <td>
              <button @click="irAEliminar(mov.id)" class="btn-eliminar">Eliminar</button>
              <button @click="irAModificar(mov.id)"
              :disabled="mov.action.toLowerCase() === 'sale'" title="Solo se pueden modificar compras" 
              class="btn-modificar">Modificar</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>    
</template>

<script>
export default {
  name: "historial-movimientos",
  data() {
    return {
      movimientos: []  
    };
  },
  mounted() {
    fetch("https://localhost:7116/transactions")
      .then(response => {
        if (!response.ok) throw new Error("Error al obtener el historial");
        return response.json();
      })
      .then(data => {
        console.log("Datos recibidos:", data);
        this.movimientos = data;  // asignar a movimientos
      })
      .catch(error => {
        console.error("Error al obtener historial:", error);
      });
  },
  methods: {
    irAEliminar(id) {
      this.$router.push(`/eliminar-operacion/${id}`);
    },
    irAModificar(id) {
      this.$router.push(`/modificar-operacion/${id}`);
    },
    volverMenu() {
      this.$router.push('/');
    }
  }
};
</script>
