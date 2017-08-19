using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoMvc.Data;

namespace TodoMvc.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _appDbContext;
        public IndexModel(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            Todos = _appDbContext.Todos.
            OrderBy(t => t.IsCompleted).ToList();
            Todo = new Todo();
        }

        [BindProperty]
        public Todo Todo { get; set; }
        public List<Todo> Todos { get; private set; }
        public void OnGet()
        {
            Todos = _appDbContext.Todos.
            OrderBy(t => t.IsCompleted).ToList();
            Todo = new Todo();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _appDbContext.Todos.Add(Todo);
            _appDbContext.SaveChanges();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostMarkAsCompleted(int id)
        {
            var todo = _appDbContext.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return Page();
            }

            todo.IsCompleted = !todo.IsCompleted;
            _appDbContext.Attach(todo).State = EntityState.Modified;
            _appDbContext.SaveChanges();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostRemoveTodo(int id)
        {
            var todo = _appDbContext.Todos.FirstOrDefault(t => t.Id == id);
            if (todo == null)
            {
                return Page();
            }

            _appDbContext.Todos.Remove(todo);
            _appDbContext.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}
