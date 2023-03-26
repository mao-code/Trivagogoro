from django.urls import path
from . import views
from django.views.generic.base import RedirectView

urlpatterns = [
    path('', RedirectView.as_view(url='mappage/')),
    path('mappage/', views.mappage, name='mappage'),
]