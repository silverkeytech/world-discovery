CREATE MIGRATION m1k2raseiagy4hczgh77fclms5dyua3kowtgrrwthamqhut2t2nihq
    ONTO m1rhay63ojqdgk5jcaj3gtd5n6gxvthusng3k5qa35ddxjglczvfuq
{
  ALTER TYPE default::Place {
      CREATE PROPERTY location_url: std::str;
  };
};
