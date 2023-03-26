from django.shortcuts import render, redirect
from django.http import HttpResponse
from django.template import loader
from user.models import User, UserCredential
from user.forms import UserForm, UserCredentialForm

def login(request):
    template = loader.get_template('login.html')
    return HttpResponse(template.render())

# Register User
# Both handle POST and GET Request
def register(request):
    if request.method == 'POST':
        user_form = UserForm(request.POST)
        user_credential_form = UserCredentialForm(request.POST)
        if user_form.is_valid() and user_credential_form.is_valid():
            user_form.save()
            user_credential_form.save()
            return redirect('/')
    else:
        # TESTING
        user_form = UserForm()
        user_credential_form = UserCredentialForm()
    return render(request, 'register.html', {
        'user_form': user_form, 
        'user_credential_form': user_credential_form
    })


