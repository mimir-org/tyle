import { ComponentMeta, ComponentStory } from "@storybook/react";
import { mockNodeTerminalItem } from "common/utils/mocks";
import { TerminalTable } from "features/common/terminal/table/TerminalTable";

export default {
  title: "Features/Common/Terminal/TerminalTable",
  component: TerminalTable,
  args: {
    terminals: [...Array(7)].map((_) => mockNodeTerminalItem()),
  },
} as ComponentMeta<typeof TerminalTable>;

const Template: ComponentStory<typeof TerminalTable> = (args) => <TerminalTable {...args} />;

export const Default = Template.bind({});
