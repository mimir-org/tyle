import { StoryFn } from "@storybook/react";
import { Text } from "complib/text/Text";

export default {
  title: "Text/Text",
  component: Text,
  args: {
    children: "A pretty lengthy text displaying the capabilities of this component.",
  },
};

const Template: StoryFn<typeof Text> = (args) => <Text {...args}></Text>;

export const Paragraph = Template.bind({});
Paragraph.args = {
  as: "p",
};

export const Small = Template.bind({});
Small.args = {
  as: "small",
};

export const Bold = Template.bind({});
Bold.args = {
  as: "b",
};

export const Idiomatic = Template.bind({});
Idiomatic.args = {
  as: "i",
};

export const Emphasized = Template.bind({});
Emphasized.args = {
  as: "em",
};

export const WithEllipsis = Template.bind({});
WithEllipsis.args = {
  ...Paragraph.args,
  useEllipsis: true,
  ellipsisMaxLines: 2,
};
WithEllipsis.decorators = [
  (Story) => (
    <div>
      <Bold>Container width: 150px</Bold>
      <br />
      <div
        style={{
          width: "150px",
          border: "2px solid red",
        }}
      >
        <Story />
      </div>
    </div>
  ),
];
