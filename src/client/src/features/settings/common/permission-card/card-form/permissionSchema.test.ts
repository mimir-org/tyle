import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { permissionSchema } from "features/settings/common/permission-card/card-form/permissionSchema";
import { FormUserPermission } from "features/settings/common/permission-card/card-form/types/formUserPermission";

describe("accessSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a user", async () => {
    const userPermission: Partial<FormUserPermission> = {
      companyId: 0,
      permission: { value: MimirorgPermission.None, label: "None" },
    };
    await expect(permissionSchema(t).validateAt("userId", userPermission)).rejects.toBeTruthy();
  });

  it("should reject without a company", async () => {
    const userPermission: Partial<FormUserPermission> = {
      userId: "someId",
      permission: { value: MimirorgPermission.None, label: "None" },
    };
    await expect(permissionSchema(t).validateAt("companyId", userPermission)).rejects.toBeTruthy();
  });

  it("should reject without a permission", async () => {
    const userPermission: Partial<FormUserPermission> = {
      userId: "someId",
      companyId: 0,
    };
    await expect(permissionSchema(t).validateAt("permission", userPermission)).rejects.toBeTruthy();
  });
});
