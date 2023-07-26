module default {
    type User {
        required first_name: str;
        required last_name: str;
        required email: str {
            constraint exclusive;
        };
        required password: str;
        required join_date: datetime;
        multi uploaded_sites: Site;
    }

    type Site {
        required name: str {
            constraint exclusive;
        };
        required category: str;
        image: Image;
        required about: str;
        facebook_link: str;
        website_link: str;
        required address: str;
        required email: str;
        required phone_number: str;
        multi sections: Section;
        required labels: array<str>;
    }

    type Image {
        required property filename -> str;
        required property url -> str;
    }

    type Section {
        required title: str;
        required description: str;
    }
}
