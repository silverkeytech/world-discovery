CREATE MIGRATION m17rw5clbqxpgymqw3hir3i64j5oie7due5sxpwhhmkdtbwkv6rdbq
    ONTO initial
{
  CREATE TYPE default::Address {
      CREATE REQUIRED PROPERTY city: std::str;
      CREATE REQUIRED PROPERTY country: std::str;
      CREATE PROPERTY google_map: std::str;
      CREATE REQUIRED PROPERTY street_name: std::str;
      CREATE REQUIRED PROPERTY street_number: std::int32;
  };
  CREATE TYPE default::Image {
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
      CREATE REQUIRED PROPERTY url: std::str;
  };
  CREATE TYPE default::LabelCategory {
      CREATE REQUIRED PROPERTY background: std::str;
      CREATE REQUIRED PROPERTY font_color: std::str;
      CREATE REQUIRED PROPERTY name: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
  };
  CREATE TYPE default::Label {
      CREATE REQUIRED LINK category: default::LabelCategory;
      CREATE REQUIRED PROPERTY name: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
  };
  CREATE TYPE default::Section {
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
  };
  CREATE TYPE default::Place {
      CREATE REQUIRED LINK address: default::Address;
      CREATE LINK place_image: default::Image;
      CREATE MULTI LINK labels: default::Label;
      CREATE MULTI LINK sections: default::Section;
      CREATE REQUIRED PROPERTY category: std::str;
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE PROPERTY facebook_link: std::str;
      CREATE REQUIRED PROPERTY last_updated: std::datetime;
      CREATE REQUIRED PROPERTY name: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
      CREATE REQUIRED PROPERTY phone_number: std::str;
      CREATE PROPERTY website_link: std::str;
  };
  CREATE TYPE default::User {
      CREATE MULTI LINK uploaded_places: default::Place;
      CREATE REQUIRED PROPERTY email: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY join_date: std::datetime;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY password: std::str;
  };
  ALTER TYPE default::Place {
      CREATE REQUIRED LINK created_by: default::User;
  };
};
