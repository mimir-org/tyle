import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { permissionSchema } from "components/PermissionCard/permissionSchema";
import { FormUserPermission } from "components/PermissionCard/formUserPermission";
import { vi } from "vitest";

describe("accessSchema tests", () => {
  const t = vi.fn();

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
