CREATE MIGRATION m1eh236o3klbcvtzysh5nttew6muvtznlwgapqjg3mevulik34ilea
    ONTO m1w4y2sq7xsj3uzfypl6u4we7gc3xcpyvhyd7uh3byz6k3nlqhro6q
{
  ALTER TYPE default::Place {
      DROP PROPERTY created_by;
  };
  ALTER TYPE default::Place {
      CREATE REQUIRED LINK created_by: default::User {
          SET REQUIRED USING (<default::User>{});
      };
  };
};
