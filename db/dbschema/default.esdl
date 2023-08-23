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
        required description: str;
        multi labels: Label;

        facebook_link: str;
        website_link: str;
        required email: str;
        required phone_number: str;

        required address: Address;
        place_image: Image;
        multi sections: Section;

        required created_by: User;
        required last_updated: datetime;
    }

    type Label {
        required name: str {
            constraint exclusive;
        };
        required category: LabelCategory;
    }

    type LabelCategory {
        required name: str {
            constraint exclusive;
        };
        required background: str;
        required font_color: str;
    }

    type Address {
        required street_number: int32;
        required street_name: str;
        required neighbourhood: str;
        required city: str;
        required country: str;
        google_map: str;
    }

    type Image {
        required title: str;
        required description: str;
        required url: str;
    }

    type Section {
        required title: str;
        required description: str;
    }
}
