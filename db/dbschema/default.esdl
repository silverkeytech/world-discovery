module default {
    type User {
        required first_name: str;
        required last_name: str;
        required email: str {
            constraint exclusive;
        };
        required password: str;
        required join_date: datetime;
        multi uploaded_places: Place;
    }

    type Place {
        required name: str {
            constraint exclusive;
        };
        required category: str;
        required created_by: str;
        required last_updated: datetime;
        required description: str;
        place_image: bytes;
        facebook_link: str;
        website_link: str;
        google_map: str;
        required address: str;
        required email: str;
        required phone_number: str;
        multi sections: Section;
        multi labels: Label;
    }

    type Label {
        required name: str;
        required category: LabelCategory;
    }

    type LabelCategory {
        required property name: str;
        required property color: str;
    }

    type Section {
        required title: str;
        required description: str;
    }
}
