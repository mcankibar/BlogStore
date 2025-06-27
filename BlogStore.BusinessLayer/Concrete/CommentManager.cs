using BlogStore.BusinessLayer.Abstract;
using BlogStore.DataAccessLayer.Abstract;
using BlogStore.EntityLayer.Entities;

namespace BlogStore.BusinessLayer.Concrete;

public class CommentManager : ICommentService
{
    private readonly   ICommentDal _commentDal;
    public CommentManager(ICommentDal commentDal)
    {
        _commentDal = commentDal;
    }
    public void TInsert(Comment entity)
    {
        if (entity.CommentDetail.Length >= 5 && entity.CommentDetail.Length <= 500)
        {
            _commentDal.Insert(entity);
        }
        else
        {
            throw new ArgumentException("Comment text must be between 5 and 500 characters long.");
        }
    }

    public void TUpdate(Comment entity)
    {
        if (entity.CommentDetail.Length >= 5 && entity.CommentDetail.Length <= 500)
        {
            _commentDal.Update(entity);
        }
        else
        {
            throw new ArgumentException("Comment text must be between 5 and 500 characters long.");
        }
    }

    public void TDelete(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid comment ID.");
        }
        
        _commentDal.Delete(id);
    }

    public List<Comment> TGetAll()
    {
        return _commentDal.GetAll();
    }

    public Comment TGetById(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid comment ID.");
        }
        
        return _commentDal.GetById(id);
    }

    public List<Comment> TGetCommentsByArticle(int id)
    {
        return _commentDal.GetCommentsByArticle(id);
    }
}