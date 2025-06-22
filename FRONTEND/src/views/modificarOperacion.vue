<template> 
  <div class="fondo">
    <div class="modificar">
      <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
      <h2>Modificar Operación</h2>
      <form @submit.prevent="guardarCambios">
        <div>
          <label>Criptomoneda:</label>
          <select v-model="cryptoCode" required>
            <option disabled value="">Seleccionar</option>
            <option value="BTC">Bitcoin</option>
            <option value="ETH">Ethereum</option>
            <option value="LTC">Litecoin</option>
          </select>
        </div>

        <div>
          <label>Monto:</label>
          <input v-model.number="cryptoAmount" type="number" min="0" step="any" />
        </div>

        <div class="acciones">
          <button type="submit" class="btnGuardar">Guardar cambios</button>
          <button @click.prevent="cancelar" class="btnCancelar">Cancelar</button>
        </div>
      </form>

      <div :class="['message', { show: message }]">
        {{ message }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      id: null,
      cryptoCode: '',
      cryptoAmount: 0,
      action: '',
      message: ''
    };
  },
  mounted() {
    this.id = this.$route.params.id;

    // Obtener datos existentes de la operación
    fetch(`https://localhost:7116/transactions/${this.id}`)
      .then(res => {
        if (!res.ok) throw new Error("No se encontró la transacción");
        return res.json();
      })
      .then(data => {
        this.cryptoCode = data.cryptoCode;
        this.cryptoAmount = data.cryptoAmount;
        this.action = data.action; // Guardar la acción original
      })
      .catch(error => {
        console.error("Error al cargar la transacción:", error);
      });
  },
  methods: {
    guardarCambios() {
      if (!this.cryptoCode) {
        this.message = "Debe seleccionar una criptomoneda válida";
        return;
      }
      if (this.cryptoAmount <= 0) {
        this.message = "El monto debe ser mayor a cero";
        return;
      }

      if (this.action.toLowerCase() == 'sale') {
        this.message = "Solo se pueden modificar compras.";
        return;
      }

      const cambios = {
        CryptoCode: this.cryptoCode,
        CryptoAmount: this.cryptoAmount,
        Action: this.action
      };

      fetch(`https://localhost:7116/transactions/${this.id}`, {
        method: "PATCH",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(cambios)
      })
        .then(async res => {
          if (!res.ok) {
            const errorText = await res.text();
            throw new Error(`Error ${res.status}: ${errorText}`);
          }
          return res.json();
        })
        .then(() => {
          this.message = "Transacción actualizada con éxito";
          setTimeout(() => {
            this.$router.push("/historial-movimientos");
          }, 2000);
        })
        .catch(error => {
          console.error("Error al guardar cambios:", error);
          this.message = `Ocurrió un error al guardar los cambios: ${error.message}`;
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
