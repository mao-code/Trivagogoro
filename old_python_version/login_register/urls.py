from django.urls import path
from . import views
from django.views.generic.base import RedirectView

urlpatterns = [
    path('', views.login, name='login'),
    path('register/', views.register, name='register')
]