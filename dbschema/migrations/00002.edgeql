CREATE MIGRATION m1rhay63ojqdgk5jcaj3gtd5n6gxvthusng3k5qa35ddxjglczvfuq
    ONTO m17rw5clbqxpgymqw3hir3i64j5oie7due5sxpwhhmkdtbwkv6rdbq
{
  ALTER TYPE default::Address {
      CREATE REQUIRED PROPERTY neighbourhood: std::str {
          SET REQUIRED USING (<std::str>{});
      };
  };
};
