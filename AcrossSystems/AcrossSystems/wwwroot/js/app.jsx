class Article extends React.Component {

    constructor(props) {
        super(props);
        this.state = { data: props.article };
        this.onClick = this.onClick.bind(this);
    }
    onClick(e) {
        this.props.onRemove(this.state.data);
    }
    render() {
        return <div>
            <p><b>Заголовок: {this.state.data.title}</b></p>
            <p>Текст: {this.state.data.text}</p>
            <p>Дата создания по UTC: {this.state.data.createdDate}</p>
            <p><button onClick={this.onClick}>Удалить</button></p>
        </div>;
    }
}


class ArticleForm extends React.Component {

    constructor(props) {
        super(props);
        this.state = { title: "", text: "" };

        this.onSubmit = this.onSubmit.bind(this);
        this.onTitleChange = this.onTitleChange.bind(this);
        this.onTextChange = this.onTextChange.bind(this);
    }
    onTitleChange(e) {
        this.setState({ title: e.target.value });
    }
    onTextChange(e) {
        this.setState({ text: e.target.value });
    }
    onSubmit(e) {
        e.preventDefault();
        var articleTitle = this.state.title.trim();
        var articleText = this.state.text;
        if (!articleTitle || !articleText) {
            alert('Введите заголовок и текст статьи');
            return;
        }
        this.props.onArticleSubmit({ title: articleTitle, text: articleText });
        this.setState({ title: "", text: "" });
    }
    render() {
        return (
            <form onSubmit={this.onSubmit}>
                <p>
                    <input type="text"
                        placeholder="Заголовок статьи"
                        value={this.state.title}
                        onChange={this.onTitleChange} />
                </p>
                <p>
                    <textarea rows="10" cols="45"
                        placeholder="Текст статьи"
                        value={this.state.text}
                        onChange={this.onTextChange}>
                    </textarea>
                </p>
                <input type="submit" value="Сохранить" />
            </form>
        );
    }
}


class ArticlesList extends React.Component {

    constructor(props) {
        super(props);
        this.state = { articles: [] };

        this.onAddArticle = this.onAddArticle.bind(this);
        this.onRemoveArticle = this.onRemoveArticle.bind(this);
    }

    loadData() {
        var xhr = new XMLHttpRequest();
        xhr.open("get", this.props.apiUrl, true);
        xhr.onload = function () {
            var data = JSON.parse(xhr.responseText);
            this.setState({ articles: data });
        }.bind(this);
        xhr.send();
    }
    componentDidMount() {
        this.loadData();
    }
    // добавление объекта
    onAddArticle(article) {
        if (article) {
            const data = new FormData();
            data.append("title", article.title);
            data.append("text", article.text);
            var xhr = new XMLHttpRequest();

            xhr.open("post", this.props.apiUrl, true);
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send(data);
        }
    }
    
    onRemoveArticle(article) {
        if (article) {
            var url = this.props.apiUrl + "/" + article.id;

            var xhr = new XMLHttpRequest();
            xhr.open("delete", url, true);
            xhr.setRequestHeader("Content-Type", "application/json");
            xhr.onload = function () {
                if (xhr.status === 200) {
                    this.loadData();
                }
            }.bind(this);
            xhr.send();
        }
    }
    render() {

        var remove = this.onRemoveArticle;
        return <div>
            <ArticleForm onArticleSubmit={this.onAddArticle} />
            <h2>Список статей</h2>
            <div>
                {
                    this.state.articles.map(function (article) {
                        return <Article key={article.id} article={article} onRemove={remove} />
                    })
                }
            </div>
        </div>;
    }
}

ReactDOM.render(
    <ArticlesList apiUrl="/api/articles" />,
    document.getElementById("content")
);