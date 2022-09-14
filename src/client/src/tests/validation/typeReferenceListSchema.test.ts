import { TypeReferenceAm } from "@mimirorg/typelibrary-types";
import { typeReferenceListSchema } from "../../content/forms/common/validation/typeReferenceListSchema";

describe("typeReferenceListSchema tests", () => {
  it("should reject typeReferences without a name", async () => {
    const typeReferencesWithoutNames: Partial<TypeReferenceAm>[] = [{ name: "" }];
    await expect(
      typeReferenceListSchema("Reference name required").validate(typeReferencesWithoutNames)
    ).rejects.toBeTruthy();
  });
});
