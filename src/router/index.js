import { createRouter, createWebHistory } from 'vue-router'
import LandingPage from '@/components/LandingPage.vue'
import compraVenta from '../views/compraVenta.vue'
import historialMovimientos from '@/views/historialMovimientos.vue'
import eliminarOperacion from '@/views/eliminarOperacion.vue'
import modificarOperacion from '@/views/modificarOperacion.vue'
import saldoActual from '@/views/saldoActual.vue'

const routes = [
  { path: '/', component: LandingPage },
  { path: '/compra-venta', component: compraVenta },
  {path: '/historial-movimientos', component: historialMovimientos},
  {path: '/eliminar-operacion/:id', component: eliminarOperacion},
  {path: '/modificar-operacion/:id', component: modificarOperacion},
  {path: '/saldo-actual', component: saldoActual}
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

export default router
