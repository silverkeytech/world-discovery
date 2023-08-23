CREATE MIGRATION m15oa37kwfxghefga3a6gbmezfcswqrpyub6tunkf5fduduz5ejxla
    ONTO m1eh236o3klbcvtzysh5nttew6muvtznlwgapqjg3mevulik34ilea
{
  ALTER TYPE default::Address {
      CREATE REQUIRED PROPERTY neighbourhood: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
