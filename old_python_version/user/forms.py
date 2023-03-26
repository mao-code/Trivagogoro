from django import forms
from django.forms import ModelForm
from .models import User, UserCredential

class UserForm(forms.ModelForm):  
    class Meta:  
        model = User
        fields = "__all__"  

class UserCredentialForm(forms.ModelForm):  
    class Meta:  
        model = UserCredential
        fields = "__all__"  