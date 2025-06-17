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
                    <td>{{ mov.fecha }}</td>
                    <td>{{ mov.accion}}</td>
                    <td>{{ mov.moneda }}</td>
                    <td>{{ mov.cantidad }}</td>
                    <td>${{ mov.valor }}</td>
                    <td>
                    <button @click="irAEliminar(mov.id)" class="btn-eliminar">Eliminar</button>
                    <button @click="irAModificar(mov.id)" class="btn-modificar">Modificar</button>
                    </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>    
</template>

<script>
export default {
  name: "historialMovimientos",
  data() {
    return {
      movimientos: [
        {
          id: 'a1',
          fecha: "2025-06-12",
          accion: "Compra",
          moneda: "BTC",
          cantidad: 0.01,
          valor: 300000
        },
        {
          id: 'a2',
          fecha: "2025-06-10",
          accion: "Venta",
          moneda: "ETH",
          cantidad: 0.2,
          valor: 100000
        }
      ]
    };
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

<style scoped>
.fondo {
  background-image: url("@/assets/img-cripto.jpg");
  background-size: cover;
  background-position: center;
  min-height: 100vh;
  display: flex;
  justify-content: center;
}

.historial {
  padding: 2rem;
  background-color: rgba(255, 255, 255, 0.95);
  border-radius: 10px;
  max-width: 800px;
  margin: 4rem auto;
  width: 80%;
  box-shadow: 0 2px 10px rgba(128, 0, 128, 0.7);
  border: 0.3rem;
}
h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  color: black;
  font-size: 2.5rem;
}
table {
  width: 100%;
  border-collapse: collapse;
  padding: 2rem;
}
th, td {
  padding: 0.8rem;
  border-bottom: 1px solid #ddd;
  text-align: center;
}
th {
  background-color: #b983ff;
  color: black;
}

.btn-eliminar, .btn-modificar {
  margin: 0.3rem;
  padding: 0.3rem auto;
  background-color: rgb(134, 53, 171);
  border: none;
  cursor: pointer;
  color: white;
  font-weight: bold;
  border-radius: 5px;
  item-align: center;
  transition: background-color 0.3s, transform 0.4s;
}

.btn-eliminar:hover, .btn-modificar:hover {
  background-color: rgb(156, 104, 181);
  transform: scale(1.1);
  box-shadow: 0 4px 10px rgba(101, 11, 103, 0.6);
}

.btn-volver {
  margin: 0.3rem;
  padding: 0.3rem 1rem;
  background-color: rgb(134, 53, 171);
  border: none;
  cursor: pointer;
  color: white;
  font-weight: bold;
  border-radius: 5px;
  transition: background-color 0.3s, transform 0.4s;
}

.btn-volver:hover {
  background-color: rgb(156, 104, 181);
  transform: scale(1.1);
  box-shadow: 0 4px 10px rgba(101, 11, 103, 0.6);
}
</style>
