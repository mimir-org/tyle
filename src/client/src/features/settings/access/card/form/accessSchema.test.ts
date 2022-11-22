import { MimirorgPermission } from "@mimirorg/typelibrary-types";
import { accessSchema } from "features/settings/access/card/form/accessSchema";
import { FormUserPermission } from "features/settings/access/card/form/types/formUserPermission";

describe("accessSchema tests", () => {
  const t = (key: string) => key;

  it("should reject without a user", async () => {
    const userPermission: Partial<FormUserPermission> = {
      companyId: 0,
      permission: { value: MimirorgPermission.None, label: "None" },
    };
    await expect(accessSchema(t).validateAt("userId", userPermission)).rejects.toBeTruthy();
  });

  it("should reject without a company", async () => {
    const userPermission: Partial<FormUserPermission> = {
      userId: "someId",
      permission: { value: MimirorgPermission.None, label: "None" },
    };
    await expect(accessSchema(t).validateAt("companyId", userPermission)).rejects.toBeTruthy();
  });

  it("should reject without a permission", async () => {
    const userPermission: Partial<FormUserPermission> = {
      userId: "someId",
      companyId: 0,
    };
    await expect(accessSchema(t).validateAt("permission", userPermission)).rejects.toBeTruthy();
  });
});
