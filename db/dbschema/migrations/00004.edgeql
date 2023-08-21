CREATE MIGRATION m1w4y2sq7xsj3uzfypl6u4we7gc3xcpyvhyd7uh3byz6k3nlqhro6q
    ONTO m1divuzjiwyt35fa2sbzggyab6gqjjq5ql5am4xutwknjxeafj23pa
{
  ALTER TYPE default::Label {
      ALTER PROPERTY name {
          CREATE CONSTRAINT std::exclusive;
      };
  };
  ALTER TYPE default::LabelCategory {
      ALTER PROPERTY name {
          CREATE CONSTRAINT std::exclusive;
      };
  };
  ALTER TYPE default::Section {
      DROP PROPERTY position;
  };
};
