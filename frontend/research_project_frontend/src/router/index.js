import Vue from 'vue'
import VueRouter from 'vue-router'

Vue.use(VueRouter)

const routes = [
  {
    path: '/login',
    name: 'Login',
    component: () => import ('../views/Login')
  },
  {
    path:'/',
    name: 'Overzicht',
    meta: {title: 'Overzicht - Stageopdrachten'},
    component: () => import('../views/Overview')
  },
  {
    path: '/extrainfo',
    meta: {title: 'Info - Stageopdracht'},
    name: 'ExtraInfoOpdracht',
    component: () => import('../views/ExtraInfoAssignment')
  },
  {
    path: '/bedrijven',
    name: 'Bedrijven',
    meta: {title: 'Overzicht - Bedrijven'},
    component: () => import('../views/internshipcoordinator/Companies')
  },
  {
    path: '/bedrijfinfo',
    name: 'BedrijfInfo',
    meta: {title: 'Info - Bedrijf'},
    component: () => import('../views/internshipcoordinator/CompanyInfo')
  },
  {
    path: '/studenten',
    name: 'Studenten',
    meta: {title: 'Overzicht - Student'},
    component: () => import('../views/internshipcoordinator/Students')
  },
  {
    path: '/studentinfo',
    name: 'studentInfo',
    meta: {title: 'Info - Student'},
    component: () => import('../views/internshipcoordinator/StudentInfo')
  },
  {
    path: '/nieuweopdracht',
    name: 'NieuweOpdracht',
    component: () => import ('../views/company/NewAssignment')
  },
  {
    path: '/*',
    name: 'NotFound',
    component: () => import ('../views/NotFound')
  }
]

export const router = new VueRouter({
  routes
});

router.beforeEach((to, from, next) => {
  const publicPages = ['/login'];
  const authRequired = !publicPages.includes(to.path);
  const loggedIn = localStorage.getItem('user');

  if(authRequired && !loggedIn) {
    return next('/login');
  }
  next();
});

export default router;
