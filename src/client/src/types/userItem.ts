import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { Option } from "utils";

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
