import { valueObjectListSchema } from "../../features/entities/common/validation/valueObjectListSchema";
import { ValueObject } from "../../features/entities/types/valueObject";

describe("valueObjectListSchema tests", () => {
  it("should reject empty value objects", async () => {
    const emptyValueObjects: ValueObject<string>[] = [{ value: "" }];
    await expect(valueObjectListSchema("Value required").validate(emptyValueObjects)).rejects.toBeTruthy();
  });
});
