import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "common/utils/getOptionsFromEnum";

export interface UserItem {
  id: string;
  name: string;
  email: string;
  purpose: string;
  permissions: { [key: string]: Option<MimirorgPermission> };
  company: {
    id: number;
    name: string;
  };
}
