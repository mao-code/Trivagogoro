from django.shortcuts import render, redirect
from django.http import HttpResponse
from django.template import loader
from .models import User
from .forms import UserForm

# Create your views here.
# def user(request, user_id):
#     user = User.objects.get(id = user_id)
#     template = loader.get_template('user.html')

#     return render(request, 'user.html', { 
#         'user': user
#     })

def user(request):
    template = loader.get_template('user.html')
    return HttpResponse(template.render())
