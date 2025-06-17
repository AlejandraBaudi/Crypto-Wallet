<template>
    <div class="fondo">
        <div class="modificar">
            <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
            <h2>Modificar Operación</h2>
            <form @submit.prevent="guardarCambios">
                <div>
                    <label>Accion:</label>
                    <select v-model="selectedAction">
                        <option disabled value="">Seleccionar</option>
                        <option>Comprar</option>
                        <option>Vender</option>
                    </select>
                </div>

                <div>
                    <label>Monto:</label>
                    <input type="number" id="amount" v-model.number="amount" min="0" step="any" />
                </div>

                <div class="actions">
                    <button type="submit">Guardar</button>
                    <button @click.prevent="cancelar">Cancelar</button>
                </div>
            </form>
        </div>
    </div>
</template>

<script>
export default {
  data() {
    return {
      operacion: {
        id: null,
        accion: '',
        monto: 0
      }
    };
  },
  created() {
    const id = this.$route.params.id;
    // Simulación: buscás la operación en un array local (luego lo cambiás por API si querés)
    const mock = [
      { id: 1, descripcion: "Compra BTC", monto: 500 },
      { id: 2, descripcion: "Venta ETH", monto: 300 }
    ];
    const resultado = mock.find(op => op.id == id);
    if (resultado) {
      this.operacion = { ...resultado };
    }
  },
  methods: {
    guardarCambios() {
      console.log("Guardando:", this.operacion);
      this.$router.push('/historial-movimientos'); // volver al historial después de guardar
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
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
}

.modificar {
  max-width: 400px;
  margin: 4rem auto;
  padding: 2rem;
  background-color: rgba(255, 255, 255, 0.95);
  border-radius: 10px;
  box-shadow: 0 0 15px rgba(128, 0, 128, 0.7);
  font-family: sans-serif;
  color: black;
  width: 100%;
  max-width: 500px;
}

.modificar h2 {
  font-size: 2.5rem;
  font-family: serif;
  color: black;
  margin-bottom: 4.2rem;
  text-align: center;
}

.form {
  display: flex;
  flex-direction: column;
}

.form label {
  display: block;
  margin-top: 1rem;
  color: black;
}

input, select {
  width: 100%;
  padding: 0.5rem;
  margin-top: 0.3rem;
  border-radius: 5px;
  border: none;
  font-size: 1rem;
  margin-bottom: 0.8rem;
  background-color: rgb(134, 53, 171);
  color: white;
}

.actions {
  margin-top: 1rem;
  display: flex;
  justify-content: space-between;
}


.actions button {
  flex: 1;
  margin: 0.5rem;
  padding: 0.6rem 3rem;
  background-color: rgb(134, 53, 171);
  border: none;
  cursor: pointer;
  color: white;
  font-weight: bold;
  font-size: 0.9rem;
  border-radius: 6px;
  transition: background-color 0.3s, transform 0.4s;
  item-align: center;
}

.actions button:hover {
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