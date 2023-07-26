CREATE MIGRATION m1mbgtd5ycam3eimraecgrwajgoh2sixhy7wpjgxh5ktpyl6itubsa
    ONTO initial
{
  CREATE TYPE default::Image {
      CREATE REQUIRED PROPERTY filename: std::str;
      CREATE REQUIRED PROPERTY url: std::str;
  };
  CREATE TYPE default::Section {
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
  };
  CREATE TYPE default::Site {
      CREATE LINK image: default::Image;
      CREATE MULTI LINK sections: default::Section;
      CREATE REQUIRED PROPERTY about: std::str;
      CREATE REQUIRED PROPERTY address: std::str;
      CREATE REQUIRED PROPERTY category: std::str;
      CREATE REQUIRED PROPERTY email: std::str;
      CREATE PROPERTY facebook_link: std::str;
      CREATE REQUIRED PROPERTY labels: array<std::str>;
      CREATE REQUIRED PROPERTY name: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
      CREATE REQUIRED PROPERTY phone_number: std::str;
      CREATE PROPERTY website_link: std::str;
  };
  CREATE TYPE default::User {
      CREATE MULTI LINK uploaded_sites: default::Site;
      CREATE REQUIRED PROPERTY email: std::str {
          CREATE CONSTRAINT std::exclusive;
      };
      CREATE REQUIRED PROPERTY first_name: std::str;
      CREATE REQUIRED PROPERTY join_date: std::datetime;
      CREATE REQUIRED PROPERTY last_name: std::str;
      CREATE REQUIRED PROPERTY password: std::str;
  };
};
