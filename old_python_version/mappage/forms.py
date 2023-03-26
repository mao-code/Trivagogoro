from django import forms
from django.forms import ModelForm
from .models import Restaurant

class RestaurantForm(forms.ModelForm):  
    class Meta:  
        model = Restaurant
        fields = "__all__"  