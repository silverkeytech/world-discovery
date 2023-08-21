CREATE MIGRATION m1divuzjiwyt35fa2sbzggyab6gqjjq5ql5am4xutwknjxeafj23pa
    ONTO m1iqmxi6notto2pjiuj2ps4b7vqu5y2a56reblpaelpc7l3kzhnu4q
{
  CREATE TYPE default::Address {
      CREATE REQUIRED PROPERTY city: std::str;
      CREATE REQUIRED PROPERTY country: std::str;
      CREATE PROPERTY google_map: std::str;
      CREATE REQUIRED PROPERTY street_name: std::str;
      CREATE REQUIRED PROPERTY street_number: std::int32;
  };
  ALTER TYPE default::Place {
      DROP PROPERTY address;
  };
  ALTER TYPE default::Place {
      CREATE REQUIRED LINK address: default::Address {
          SET REQUIRED USING (<default::Address>{});
      };
  };
  CREATE TYPE default::Image {
      CREATE REQUIRED PROPERTY description: std::str;
      CREATE REQUIRED PROPERTY title: std::str;
      CREATE REQUIRED PROPERTY url: std::str;
  };
  ALTER TYPE default::Place {
      DROP PROPERTY place_image;
  };
  ALTER TYPE default::Place {
      CREATE LINK place_image: default::Image;
      DROP PROPERTY google_map;
  };
  ALTER TYPE default::LabelCategory {
      ALTER PROPERTY color {
          RENAME TO background;
      };
  };
  ALTER TYPE default::LabelCategory {
      CREATE REQUIRED PROPERTY font_color: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
  ALTER TYPE default::Section {
      CREATE REQUIRED PROPERTY position: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
