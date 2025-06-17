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
    console.log("Compra registrada:", data);
  })
  .catch(error => {
    console.error("Error al comprar:", error);
    this.message = "Error al realizar la compra";
  });
}
    },

    sell() {
      const transaccion = {
        cryptoCode: this.selectedCrypto,
        cryptoAmount: this.amount,
        action: "sale"
      };

      console.log("Transaccion de venta:", transaccion);

      this.enviarTransaccion(transaccion, 'Vendiste ${this.amount} ${this.selectedCrypto}');

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
    console.log("Venta registrada:", data);
  })
  .catch(error => {
    console.error("Error al vender:", error);
    this.message = "Error al realizar la venta";
  });
},

  volverMenu() {
    this.$router.push('/');
  }
}
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

.compra-venta {
    position: relative;
    max-width: 400px;
    margin: auto;
    padding: 5rem;
    background-color: rgba(255, 255, 255, 0.95);
    border-radius: 10px;
    box-shadow: 0 0 15px rgba(128, 0, 128, 0.7);
    font-family: sans-serif;
    color: black;;
    width: 100%;
    max-width: 400px;
}

.compra-venta h1 {
    font-size: 2.5rem;
    font-family: serif;
    color: black;
    margin-top: 1rem;
    margin-bottom: 4.2rem;
    text-align: center;
}

.form {
    display: flex;
    flex-direction: column;
}

.message {
  height: 1.5rem;        
  margin-top: 2rem;       
  color: black;;
  font-weight: bold;
  font-size: 1.1rem;
  text-align: center;
  overflow: hidden;       
  transition: opacity 0.3s ease; 
}

.message.show {
    opacity: 1;
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
  margin: 0 0.5rem;
  padding: 0.6rem;
  background-color: rgb(134, 53, 171);
  border: none;
  cursor: pointer;
  color: white;
  font-weight: bold;
  font-size: 0.9rem;
  border-radius: 6px;
  transition: background-color 0.3s, transform 0.4s;
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
