<template>
      <div class="fondo">
            <div class="eliminar">
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
                        <button class="btn confirmarEliminar" @click="mostrarConfirmacion = true">Eliminar</button>
                        <button class="btn confirmarCancelar" @click="cancelar">Cancelar</button>
                    </div>
              </div>
            </div>

            <div v-if="mostrarConfirmacion" class="confirm-modal">
                  <div class="modal-content">
                    <p>¿Eliminar?</p>
                    <div class="modal-buttons">
                      <button class="btn confirmarEliminar" @click="confirmarEliminacion">Eliminar</button>
                      <button class="btn cancelar" @click="mostrarConfirmacion = false">Cancelar</button>
                    </div>
                  </div>
              </div>
      </div>
</template>

<script>
export default {
  data() {
    return {
      id: null,
      movimiento: null,
      mostrarConfirmacion: false
    };
  },
  mounted() {
    this.id = this.$route.params.id;

    // Datos de la transacción para mostrarlos
    fetch(`https://localhost:7116/transactions/${this.id}`)
      .then(response => {
        if (!response.ok) throw new Error("No se encontró la transacción");
        return response.json();
      })
      .then(data => {
        this.movimiento = {
          nombre: data.cryptoCode,
          cantidad: data.cryptoAmount,
          fecha: new Date(data.dateTime).toLocaleString()
        };
      })
      .catch(error => {
        console.error("Error al cargar el movimiento:", error);
      });
  },
  methods: {
    confirmarEliminacion() {
      fetch(`https://localhost:7116/transactions/${this.id}`, {
        method: "DELETE"
      })
        .then(res => {
          if (!res.ok) throw new Error("No se pudo eliminar la transacción");
          alert("Transacción eliminada correctamente");
          this.$router.push("/historial-movimientos");
        })
        .catch(err => {
          console.error("Error al eliminar:", err);
          alert("Error al eliminar la transacción");
        });
    },
    cancelar() {
      this.$router.push("/historial-movimientos");
    },
    volverMenu() {
      this.$router.push("/");
    }
  }
};
</script>
