<template>
  <div class="fondo">
    <div class="saldo">
      <button class="btn-volver" @click="volverMenu">Volver al menú principal</button>
      <h2>Saldo Actual</h2>
      <div class="resumen">
        <p><strong>Total en pesos:</strong> ${{ saldoTotal }}</p>
        <ul>
          <li v-for="(cripto, index) in criptomonedas" :key="index">
            {{ cripto.nombre }}: {{ cripto.cantidad }} (equivale a ${{ cripto.valorPesos }})
          </li>
        </ul>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'saldoActual',
  data() {
    return {
      saldoTotal: 420000,
      criptomonedas: [
        { nombre: 'Bitcoin', cantidad: 0.01, valorPesos: 300000 },
        { nombre: 'Ethereum', cantidad: 0.2, valorPesos: 120000 }
      ]
    };
  },
  methods: {
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
.saldo {
  padding: 2rem;
  border-radius: 10px;
  max-width: 800px;
  background-color: rgba(255, 255, 255, 0.95);
  margin: 4rem auto;
  width: 80%;
  box-shadow: 0 2 10px rgba(128, 0, 128, 0.7);
  border: 0.3rem;
}
h2 {
  text-align: center;
  margin-bottom: 1rem;
  color: black;
  font-size: 2.5rem;
}
.resumen p {
  font-size: 1.4rem;
  margin-bottom: 1rem;
}
.resumen ul {
  list-style-type: none;
  padding: 0;
}
.resumen li {
  background-color:rgb(185, 152, 232);
  padding: 0.5rem;
  font-size: 1.2rem;
  margin-bottom: 0.5rem;
  border-radius: 6px;
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
}
</style>
