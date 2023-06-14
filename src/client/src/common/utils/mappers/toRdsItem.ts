import { RdsLibCm } from "@mimirorg/typelibrary-types";
import { RdsItem } from "../../types/rdsItem";

export const toRdsItem = (rds: RdsLibCm): RdsItem => {
  return {
    id: rds.id,
    name: rds.name,
    description: rds.description,
    kind: "RdsItem",
    state: rds.state,
    rdsCode: rds.rdsCode,
  };
};
