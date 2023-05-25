import { faker } from "@faker-js/faker";
import { Meta, StoryFn } from "@storybook/react";
import { mockAspectObjectTerminalItem } from "common/utils/mocks";
import { LibraryIcon } from "complib/assets";
import { AspectObjectPreview } from "features/entities/entityPreviews/aspectobject/AspectObjectPreview";

export default {
  title: "Features/Common/AspectObject/AspectObjectPreview",
  component: AspectObjectPreview,
} as Meta<typeof AspectObjectPreview>;

const Template: StoryFn<typeof AspectObjectPreview> = (args) => <AspectObjectPreview {...args} />;

export const Default = Template.bind({});
Default.args = {
  name: "Aspect object",
  color: faker.helpers.arrayElement(["#fef445", "#00f0ff", "#fa00ff"]),
  img: LibraryIcon,
  terminals: [...Array(7)].map((_) => mockAspectObjectTerminalItem()),
};
