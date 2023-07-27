CREATE MIGRATION m1iqmxi6notto2pjiuj2ps4b7vqu5y2a56reblpaelpc7l3kzhnu4q
    ONTO m1mbgtd5ycam3eimraecgrwajgoh2sixhy7wpjgxh5ktpyl6itubsa
{
  ALTER TYPE default::Site {
      DROP LINK image;
  };
  ALTER TYPE default::Image {
      DROP PROPERTY url;
  };
  ALTER TYPE default::Image RENAME TO default::Label;
  CREATE TYPE default::LabelCategory {
      CREATE REQUIRED PROPERTY color: std::str;
      CREATE REQUIRED PROPERTY name: std::str;
  };
  ALTER TYPE default::Label {
      CREATE REQUIRED LINK category: default::LabelCategory {
          SET REQUIRED USING (<default::LabelCategory>{});
      };
  };
  ALTER TYPE default::Label {
      ALTER PROPERTY filename {
          RENAME TO name;
      };
  };
  ALTER TYPE default::Site {
      DROP PROPERTY labels;
  };
  ALTER TYPE default::Site RENAME TO default::Place;
  ALTER TYPE default::Place {
      CREATE MULTI LINK labels: default::Label;
  };
  ALTER TYPE default::Place {
      ALTER PROPERTY about {
          RENAME TO created_by;
      };
  };
  ALTER TYPE default::Place {
      CREATE REQUIRED PROPERTY description: std::str {
          SET REQUIRED USING (<std::str>{});
      };
      CREATE PROPERTY google_map: std::str;
      CREATE REQUIRED PROPERTY last_updated: std::datetime {
          SET REQUIRED USING (<std::datetime>{});
      };
      CREATE PROPERTY place_image: std::bytes;
  };
  ALTER TYPE default::User {
      ALTER LINK uploaded_sites {
          RENAME TO uploaded_places;
      };
  };
};
