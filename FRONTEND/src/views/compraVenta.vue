<template>
    <div class="fondo">
        <div class="compra-venta">
            <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
            <h1>Compra y Venta de Criptomonedas</h1>

            <div class="form">
            <label for="crypto">Criptomoneda:</label>
                <select v-model="selectedCrypto">
                    <option disabled value="">Seleccionar</option>
                    <option value="BTC">Bitcoin (btc)</option>
                    <option value="ETH">Ethereum (eth)</option>
                    <option value="LTC">Litecoin (ltc)</option>
                </select>


                <label for="amount">Cantidad:</label>
                <input type="number" id="amount" v-model.number="amount" min="0" />

                <div class="actions">
                    <button @click="buy">Comprar</button>
                    <button @click="sell">Vender</button>
                </div>

                <div :class="['message', { show: message }]">
                    {{ message }}
                </div>

            </div>
        </div>
    </div>
</template>

<script>
export default {
  name: 'compra-venta',
  data() {
    return {
      cryptos: ['BTC', 'ETH', 'LTC'],
      selectedCrypto: '',
      amount: 0,
      message: ''
    };
  },
  methods: {
    buy() {
      const transaccion = {
        cryptoCode: this.selectedCrypto,
        cryptoAmount: this.amount,
        action: "purchase"
      };

      fetch("https://localhost:7116/transactions", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(transaccion)
      })
      .then(response => {
        if (!response.ok) throw new Error("No se pudo registrar la compra");
        return response.json();
      })
      .then(data => {
        this.message = `Compraste ${this.amount} ${this.selectedCrypto}`;
          setTimeout(() => {
            this.$router.push("/historial-movimientos");
          }, 2000);
        console.log("Compra registrada:", data);
      })
      .catch(error => {
        console.error("Error al comprar:", error);
        this.message = "Error al realizar la compra";
      });
    },

    sell() {
      const transaccion = {
        cryptoCode: this.selectedCrypto,
        cryptoAmount: this.amount,
        action: "sale"
      };

      fetch("https://localhost:7116/transactions", {
        method: "POST",
        headers: {
          "Content-Type": "application/json"
        },
        body: JSON.stringify(transaccion)
      })
      .then(response => {
        if (!response.ok) throw new Error("No se pudo registrar la venta");
        return response.json();
      })
      .then(data => {
        this.message = `Vendiste ${this.amount} ${this.selectedCrypto}`;
          setTimeout(() => {
            this.$router.push("/historial-movimientos");
          }, 2000);
        console.log("Venta registrada:", data);
      })
      .catch(error => {
        console.error("Error al vender:", error);
        this.message = "Saldo insuficiente";
      });
    },

    volverMenu() {
      this.$router.push('/');
    }
  }
}
</script>



