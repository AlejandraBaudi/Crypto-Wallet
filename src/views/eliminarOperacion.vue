<template>
        <div class="fondo">
            <div class="historial">
                <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
                <h2>Eliminar Operacion</h2>
            
                <div v-if="movimiento">

                    <p><strong>Criptomoneda:</strong> {{ movimiento.nombre }}</p>
                    <p><strong>Cantidad:</strong> {{ movimiento.cantidad }}</p>
                    <p><strong>Fecha:</strong> {{ movimiento.fecha }}</p>

                    <p style="margin-top: 1rem; color: red;">
                        <strong>¿Estás seguro que querés eliminar este movimiento?</strong>
                    </p>

                    <div class="botones">
                        <button class="btn eliminar" @click="confirmarEliminacion">Eliminar</button>
                        <button class="btn cancelar" @click="cancelar">Cancelar</button>
                    </div>
                </div>

                <div v-else>
                    <p>cargando...</p>
                </div>
            </div>
        </div>
</template>

<script>
export default {
  name: 'EliminarOperacion',
  data() {
    return {
      movimiento: null,
      movimientosMock: [
        { id: 'a1', nombre: 'Bitcoin', cantidad: 0.01, fecha: '2025-06-10' },
        { id: 'a2', nombre: 'Ethereum', cantidad: 0.2, fecha: '2025-06-11' }
      ]
    };
  },
  created() {
    const id = this.$route.params.id;
    this.movimiento = this.movimientosMock.find(m => m.id === id);
  },
  methods: {
    confirmarEliminacion() {
      console.log("Eliminando ID:", this.movimiento.id);
      // Tu lógica de eliminación con API va acá
    },
    cancelar() {
      this.$router.push('/historial-movimientos');
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
  align-items: center;
}

.historial {
  padding: 2rem;
  border-radius: 10px;
  max-width: 800px;
  background-color: rgba(255, 255, 255, 0.95);
  width: 80%;
  margin: 4rem auto;
  box-shadow:0 0 15px rgba(128, 0, 128, 0.7);
  border: 0.3rem;
}

.historial p {
  font-size: 1.2rem;
  font-family: serif;
}

h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  color: black;
  font-size: 2.5rem;
}

.botones {
  display: flex;
  justify-content: center;
  margin-top: 1.5rem;
  gap: 1rem;
}

.btn {
  padding: 0.6rem 1.2rem;
  border: none;
  border-radius: 5px;
  font-weight: bold;
  cursor: pointer;
  font-size: 1rem;
}

.eliminar {
  background-color: #ff4d4d;
  color: white;
  transition: background-color 0.3s, transform 0.4s;
}

.eliminar:hover {
  background-color: rgb(197, 62, 62);
  transform: scale(1.1);
  box-shadow: 0 4px 10px rgba(199, 91, 91, 0.6);
}

.cancelar {
  background-color: rgb(134, 53, 171);
  color: white;
  transition: background-color 0.3s, transform 0.4s;
}

.cancelar:hover {
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
