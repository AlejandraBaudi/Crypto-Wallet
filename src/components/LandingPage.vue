<template>
  <div class="landing">
    <div class="overlay">
      <main class="content">
        <h1 class="logo">SimuCripto</h1>
        <h2>Tu simulador de billetera virtual</h2>
        <p>Comprá criptomonedas, mirá tu saldo y aprendé sin arriesgar dinero real.</p>
        <button v-if= "!mostrarOpciones" @click="mostrarOpciones = true">Comenzar</button>
        <OpcionesMenu v-if= "mostrarOpciones" @navegar= "navegarA"/> 

      </main>
    </div>
  </div>
</template>

<script>
import OpcionesMenu from "@/components/OpcionesMenu.vue"

export default {
  name: "LandingPage",
  components: { OpcionesMenu },
  data() {
    return {
      mostrarOpciones: false
    };
  },
  methods: {
    navegarA(opcion) {
      switch (opcion) {
        case 'compra-venta':
          this.$router.push('/compra-venta');
          break;
        case 'historial-movimientos':
          this.$router.push('/historial-movimientos');
          break;
        case 'eliminar-operacion':
          this.$router.push('/eliminar-operacion');
          break;
        case 'modificar-operacion':
          this.$router.push('/modificar-operacion');
          break;
        case 'saldo-actual':
          this.$router.push('/saldo-actual');
          break;
      }
    }
  }
}
</script>

<style scoped>
.landing {
  background-image: url("@/assets/img-cripto.jpg");
  background-size: cover;
  background-position: center;
  height: 100vh;
  position: relative;
}
.overlay {
  background: rgba(255, 255, 255, 0.8);
  height: 100%;
  display: flex;
  flex-direction: column;
}
.header {
  display: flex;
  justify-content: space-between;
  padding: 1rem 2rem;
}
.logo {
  font-size: 6rem;
  color:rgb(134, 53, 171);
  text-align: center;
  width: 100%;
  display: block;
  margin-top: 2rem;
}
.content {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  text-align: center;
  padding-top: 1rem;
  margin-bottom: 4rem;
}
.content h2 {
  font-size: 3rem;
  font-family: serif;
  color: #333;
}
.content p {
  font-size: 1.5rem;
  margin-top: -0.5rem;
}
button {
  background-color:rgb(134, 53, 171);
  color: white;
  border: none;
  padding: 0.8rem 2rem;
  font-size: 1.5rem;
  border-radius: 10px;
  cursor: pointer;
  transition: 0.3s;
}
button:hover {
    background-color: rgb(156, 104, 181);
    transform: scale(1.2);
    box-shadow: 0 4px 10px rgba(101, 11, 103, 0.6);
}
</style>
